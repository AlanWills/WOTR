using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace BoardGameEditor
{
   public static class MenuItems
   {
       [MenuItem(BoardGameEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadBoardGameMenuItem()
       {
           LoadSceneSetMenuItem(BoardGameEditorConstants.SCENE_SET_PATH);
       }
   }
}