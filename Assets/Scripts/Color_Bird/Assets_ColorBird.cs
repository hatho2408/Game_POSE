 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey {


    public class Assets_ColorBird : MonoBehaviour {

    
        private static Assets_ColorBird _i; 

        public static Assets_ColorBird i {
            get { 
                if (_i == null) _i = (Instantiate(Resources.Load("CodeMonkeyAssets")) as GameObject).GetComponent<Assets_ColorBird>(); 
                return _i; 
            }
        }


     

        public Sprite s_White;

    }

}
