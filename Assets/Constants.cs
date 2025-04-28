using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class Constants //TODO: Rename to something more descriptive
    {
        public static string  AttackTrigger  = "AttackTrigger";
        public static string  JumpingTrigger = "JumpingTrigger";
        public static string  JumpButton     = "Jump";
        public static KeyCode AttackButton   = KeyCode.F;

        public enum Buttons
        {
            Attack  = KeyCode.F
          , Jumping = KeyCode.G
        };
    }

}
