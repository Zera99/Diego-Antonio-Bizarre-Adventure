using UnityEngine.UI;

namespace TextExtensions {

    public static class TextExtensions {

        public static void ChangeSkillText(this Text uiText, float val) {
            uiText.text = "" + val + "/100";
        }

    }
}
