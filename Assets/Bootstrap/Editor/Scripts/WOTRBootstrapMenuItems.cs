using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace WOTREditor.Bootstrap
{
   public static class MenuItems
   {
       [MenuItem(WOTRBootstrapEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadWOTRBootstrapMenuItem()
       {
           LoadSceneSetMenuItem(WOTRBootstrapEditorConstants.SCENE_SET_PATH);
       }
   }
}