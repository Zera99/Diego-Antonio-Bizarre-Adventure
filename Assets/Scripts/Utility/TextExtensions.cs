using UnityEngine.UI;
using UnityEngine;

namespace TextExtensions {

    public static class TextExtensions {

        public static void ChangeSkillText(this Text uiText, float val) {
            if (val < 0)
                uiText.text = "" + " Infinite";
            else
                uiText.text = "" + val + "/100";
        }

        public static void ChangeSkillText(this Text uiText, int skillVal, float val) {
            string skillName = "";

            switch (skillVal) {
                case 0:
                    skillName = "EGG";
                    break;
                case 1:
                    skillName = "JET";
                    break;
                case 2:
                    skillName = "BOW";
                    break;
                case 3:
                    skillName = "FRQ";
                    break;
            }


            if (val < 0)
                uiText.text = skillName + " Infinite";
            else
                uiText.text = skillName + "  " + val + "/100";
        }

    }
}
