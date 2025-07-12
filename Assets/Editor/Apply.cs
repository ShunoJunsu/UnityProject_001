using UnityEditor;
using UnityEngine;

public class ApplyTagToChildren : EditorWindow
{
    private string selectedTag = "Untagged"; // 기본 태그는 "Untagged"

    [MenuItem("Tools/Apply Tag To Children")]
    public static void ShowWindow()
    {
        GetWindow<ApplyTagToChildren>("Apply Tag To Children");
    }

    void OnGUI()
    {
        GUILayout.Label("Apply Tag to Selected GameObject's Children", EditorStyles.boldLabel);

        // 현재 선택된 오브젝트 가져오기
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            EditorGUILayout.HelpBox("하이라키에서 게임 오브젝트를 선택해주세요.", MessageType.Info);
            return;
        }

        EditorGUILayout.ObjectField("선택된 오브젝트", selectedObject, typeof(GameObject), true);

        // 모든 태그 목록 가져오기
        string[] allTags = UnityEditorInternal.InternalEditorUtility.tags;
        int currentTagIndex = System.Array.IndexOf(allTags, selectedTag);
        if (currentTagIndex == -1) currentTagIndex = 0; // 태그가 없으면 Untagged로

        int newTagIndex = EditorGUILayout.Popup("적용할 태그", currentTagIndex, allTags);
        selectedTag = allTags[newTagIndex];

        if (GUILayout.Button("선택된 오브젝트와 모든 자식에 태그 적용"))
        {
            ApplyTagRecursive(selectedObject.transform, selectedTag);
            Debug.Log($"'{selectedTag}' 태그가 '{selectedObject.name}'와 모든 자식에 적용되었습니다.");
        }
    }

    // 재귀적으로 모든 자식에게 태그를 적용하는 함수
    void ApplyTagRecursive(Transform parentTransform, string tag)
    {
        // 부모 오브젝트에도 태그 적용
        parentTransform.gameObject.tag = tag;

        // 모든 자식 오브젝트에 대해 반복
        foreach (Transform child in parentTransform)
        {
            ApplyTagRecursive(child, tag); // 재귀 호출
        }
    }
}