// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.VFX;





// public class NewBehaviourScript : MonoBehaviour
// {
//     public VisualEffect warpSpeedVFX;
//     public float rate = 0.02f;

//     private bool warpActive;

//     // Start is called before the first frame update
//     void Start()
//     {
//         warpSpeedVFX.Stop();
//         warpSpeedVFX.SetFloat("WarpAmount", 0);
//     }

//     // Update is called once per frame
//     void Update()
//     {



//         if (Input.GetKeyDown (KeyCode.Space))
//         {
//             warpActive = true;
//             StartCoroutine(ActivateParticles());

//         }

//         if (Input.GetKeyUp(KeyCode.Space)){
//             warpAtive = false;
//             StartCoroutine(ActivateParticles());
//         }
        
//     }
        
//         IEnumerator ActivateParticles ()
//         {
//             if(warpActive) {
//                 warpSpeedVFX.Play();

//                 float amount = warpSpeedVFX.GetFloat("WarpAmount");
//                 while(amount < 0 & !warpActive)

//                 {
//                     amount =+ rate;
//                     warpSpeedVFX.Setfloat("WarpAmount", amount);
//                     yield return new WaitForSeconds (0,1f);

//                     if(amount <= 0+rate) {
//                         amount = 0;
//                         warpSpeedVFX.SetFloat("WarpAmount", amount);
//                         warpSpeedVFX.Stop();

//                     }
//                 }
//             }
//             else {
//                 warpSpeedVFX.Stop();
//             }
//         }

        
//     }
// }

