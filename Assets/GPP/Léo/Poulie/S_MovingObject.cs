using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.TerrainTools;
#endif
using UnityEngine;

public class S_MovingObject : MonoBehaviour
{
    public enum MoveType
    {
        SmoothDamp,
        Curve
    }
    [HideInInspector]
    public MoveType moveType = MoveType.SmoothDamp;



    [SerializeField] private Transform tBegin;
    [SerializeField] private Transform tEnd;

    private Vector3 posBegin;
    private Vector3 posEnd;

    [HideInInspector]
    public float moveAccuracy = 0.2f;
    [HideInInspector]
    public float movementSmoothTime = 0.2f;

    [HideInInspector] public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [HideInInspector]
    public float curveTime = 1.0f, debugCurvePrevis = 10;

    [HideInInspector] public Color debugColor = Color.red;

    bool goinToTheEnd = false;
    bool isMovingNow;
    float moveTime = 0.0f;

    private Vector3 targetPosition;
    private Vector3 moveVelocity = Vector3.zero;

    private void Start()
    {
        isMovingNow = false;
        posBegin = tBegin.position;
        posEnd = tEnd.position;
    }

    private void Update()
    {
        if (isMovingNow)
        {

            switch (moveType)
            {
                case MoveType.SmoothDamp:
                    Debug.Log("transform : " + transform.position);
                    Debug.Log("target : " + targetPosition);
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
                    Debug.Log("transform : " + transform.position);
                    Debug.Log("target : " + targetPosition);
                    break;
                case MoveType.Curve:
                    if (goinToTheEnd)
                    {
                        moveTime += (1 / curveTime) * Time.deltaTime;
                    }
                    else
                    {
                        moveTime -= (1 / curveTime) * Time.deltaTime;
                    }

                    transform.position = Vector3.Lerp(posBegin, posEnd, moveCurve.Evaluate(moveTime));
                    break;
                default: break;
            }
            if (Vector3.Distance(transform.position, targetPosition) < moveAccuracy)
            {
                isMovingNow = false;
                transform.position = targetPosition;
               
            }
        }
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, movementSmoothTime);
    }

    public void Interact()
    {
        goinToTheEnd = !goinToTheEnd;
        if (goinToTheEnd)
        {
            targetPosition = posEnd;
        }
        else
        {
            targetPosition = posBegin;
        }
        isMovingNow = true;
    }

#if UNITY_EDITOR
    public void CreatePos()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        GameObject gB = new GameObject();
        gB.name = "Begin Position";
        gB.transform.parent = transform;
        gB.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        var iconContent = EditorGUIUtility.IconContent("sv_label_1");
        EditorGUIUtility.SetIconForObject(gB, (Texture2D)iconContent.image);
        tBegin = gB.transform;
        
        GameObject gE = new GameObject();
        gE.name = "End Position";
        gE.transform.parent = transform;
        gE.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        var iconContent2 = EditorGUIUtility.IconContent("sv_label_2");
        EditorGUIUtility.SetIconForObject(gE, (Texture2D)iconContent2.image);
        tEnd = gE.transform;
    }


    void OnDrawGizmos()
    {
        if (posEnd != null && posBegin != null)
        {
            Handles.color = debugColor;
            Handles.DrawDottedLine(tBegin.position, tEnd.position, 5);
            if (moveType == MoveType.Curve)
            {
                for (float i = 0; i < 1; i += 1 / debugCurvePrevis)
                    Handles.DrawWireDisc(Vector3.Lerp(tBegin.position, tEnd.position, moveCurve.Evaluate(i)), (tEnd.position - tBegin.position).normalized, 0.1f);
            }
            Handles.DrawWireDisc(Vector3.Lerp(tBegin.position, tEnd.position , 1), (tEnd.position - tBegin.position).normalized, 0.1f);
        }
    }
#endif

}

#if UNITY_EDITOR
[CustomEditor(typeof(S_MovingObject))]
public class Edit_Moving_Object : Editor
{

    SerializedProperty m_CurveTime;
    SerializedProperty m_MoveType;
    SerializedProperty m_MoveAccuracy;
    SerializedProperty m_MovementSmoothTime;
    SerializedProperty m_MoveCurve;

    

    //Debug Variables
    SerializedProperty m_DebugColor;
    SerializedProperty m_DebugCurvePrevis;

    void OnEnable()
    {
        var t = target as S_MovingObject;
        // Fetch the objects from the GameObject script to display in the inspector
        m_MoveType = serializedObject.FindProperty("moveType");
        m_MoveAccuracy = serializedObject.FindProperty("moveAccuracy");
        m_MovementSmoothTime = serializedObject.FindProperty("movementSmoothTime");

        m_MoveCurve = serializedObject.FindProperty("moveCurve");
        m_CurveTime = serializedObject.FindProperty("curveTime");

        m_DebugColor = serializedObject.FindProperty("debugColor");
        m_DebugCurvePrevis = serializedObject.FindProperty("debugCurvePrevis");


    }


    public override void OnInspectorGUI()
    {
        var t = target as S_MovingObject;

        DrawDefaultInspector();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Movement Customisation", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_MoveType);
        EditorGUILayout.PropertyField(m_MoveAccuracy);
        switch (m_MoveType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(m_MovementSmoothTime);
                break;
            case 1:
                EditorGUILayout.PropertyField(m_MoveCurve);
                EditorGUILayout.PropertyField(m_CurveTime);
                break;
            default:

                break;
        }

        EditorGUILayout.Space(3);

        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_DebugColor);
        if (m_MoveType.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(m_DebugCurvePrevis);
        }

        if (GUILayout.Button("Create Pos"))
        {
            t.CreatePos();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
            EditorUtility.SetDirty(t);
    }

}
#endif