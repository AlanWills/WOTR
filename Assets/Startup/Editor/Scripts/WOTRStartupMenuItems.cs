using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace WOTREditor.Startup
{
   public static class MenuItems
   {
       [MenuItem(WOTRStartupEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadWOTRStartupMenuItem()
       {
           LoadSceneSetMenuItem(WOTRStartupEditorConstants.SCENE_SET_PATH);
       }
   }
}