using UnityEngine.UI;

namespace TextExtensions {

    public static class TextExtensions {

        public static void ChangeSkillText(this Text uiText, float val) {
            if (val < 0)
                uiText.text = "" + " Infinite";
            else
                uiText.text = "" + val + "/100";
        }

        public static void ChangeSkillText(this Text uiText, string skillName, float val) {
            if (val < 0)
                uiText.text = skillName + " Infinite";
            else
                uiText.text = skillName + "  " + val + "/100";
        }

    }
}
