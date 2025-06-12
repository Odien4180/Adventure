using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(EnemyBase), true)]
public class EnemyBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var enemyBase = target as EnemyBase;
        if (GUILayout.Button("Hit"))
        {
            var floatingText = Instantiate(Resources.Load<FloatingText>("Prefabs/FloatingText"));
            floatingText.transform.position = enemyBase.transform.position + new Vector3(0, 1, 0);
            floatingText.Text.text = "Hit!";
        }
    }
}
#endif

public abstract class EnemyBase : Controllable
{
}
