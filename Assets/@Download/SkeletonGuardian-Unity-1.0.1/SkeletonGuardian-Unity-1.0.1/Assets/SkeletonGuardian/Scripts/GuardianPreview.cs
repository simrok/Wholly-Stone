using UnityEngine;
public class GuardianPreview : MonoBehaviour {
 public Animator animator; public AnimationClip[] clips; public string[] labels; public bool[] swordClips; public GameObject sword;
 public GameObject shield;
 public bool[] shieldClips;
 int selected; string query=""; Vector2 scroll; bool paused; float speed=1; float seek; bool dragging;
 void Start(){ Select(0); }
 public void Select(int i){selected=i;animator.Play(clips[i].name,0,0);animator.Update(0);if(sword)sword.SetActive(swordClips[i]);if(shield)shield.SetActive(shieldClips[i]);seek=0;}
 void OnGUI(){
  GUILayout.BeginArea(new Rect(15,15,350,Screen.height-30),GUI.skin.box);
  GUILayout.Label("SKELETON GUARDIAN · 110 ANIMATIONS");query=GUILayout.TextField(query);
  scroll=GUILayout.BeginScrollView(scroll,GUILayout.Height(Mathf.Max(150,Screen.height-290)));
  for(int i=0;i<clips.Length;i++)if(labels[i].ToLowerInvariant().Contains(query.ToLowerInvariant()) && GUILayout.Button(labels[i]))Select(i);
  GUILayout.EndScrollView();GUILayout.Label(labels[selected]);
  if(GUILayout.Button(paused?"Play":"Pause")){paused=!paused;animator.speed=paused?0:speed;}
  if(GUILayout.Button("Restart"))Select(selected);
  GUILayout.Label("Speed "+speed.ToString("F2"));speed=GUILayout.HorizontalSlider(speed,.25f,2f);animator.speed=paused?0:speed;
  float next=GUILayout.HorizontalSlider(seek,0,1);if(Mathf.Abs(next-seek)>.001f){animator.Play(clips[selected].name,0,next);animator.Update(0);}seek=next;
  GUILayout.Label("Repeat is preview-only; loops are not certified.");GUILayout.EndArea();
 }
 void Update(){if(!paused){var s=animator.GetCurrentAnimatorStateInfo(0);seek=s.normalizedTime%1;if(s.normalizedTime>=1)Select(selected);} }
 void LateUpdate(){if(!animator||!Camera.main)return;var hip=Find(animator.transform,"Hips");if(!hip)return;var focus=hip.position+Vector3.up*.15f;Camera.main.transform.position=focus+new Vector3(1.0f,.4f,3.7f);Camera.main.transform.LookAt(focus+Vector3.left*.45f);}
 public static Transform Find(Transform root,string name){if(root.name==name)return root;foreach(Transform c in root){var found=Find(c,name);if(found)return found;}return null;}
}
