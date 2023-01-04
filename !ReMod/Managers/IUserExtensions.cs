using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Core;
using VRC.UI.Elements.Menus;

namespace QuestMod
{
    public static class IUserExtension
    {
        #region Others
        public static Player GetPlayer(this string UserID)
        {
            foreach (Player player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray().ToList<Player>())
            {
                if (player.field_Private_APIUser_0.id == UserID)
                {
                    return player;
                }
            }
            return null;
        }
        public static SelectedUserMenuQM GetTarget()
        {
            return UnityEngine.Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>().FirstOrDefault().field_Private_UIPage_1.GetComponent<SelectedUserMenuQM>();
        }
        #endregion

        #region IUser
        public static Player GetPlayer(this IUser value)
        {
            return value.prop_String_0.GetPlayer();
        }
        public static VRCPlayer GetVRCPlayer(this IUser value)
        {
            return value.GetPlayer()._vrcplayer;
        }
        public static APIUser GetAPIUser(this IUser value)
        {
            return value.GetPlayer().prop_APIUser_0;
        }
        public static ApiAvatar GetApiAvatar(this IUser value)
        {
            return value.GetPlayer().prop_ApiAvatar_0;
        }
        #endregion

        #region SelectedUserMenuQM
        public static IUser SelectedIUser()
        {
            return GetTarget().field_Private_IUser_0;
        }
        public static VRCPlayer GetVRCPlayer()
        {
            return GetTarget().field_Private_IUser_0.GetPlayer()._vrcplayer;
        }
        public static APIUser GetAPIUser()
        {
            return GetTarget().field_Private_IUser_0.GetPlayer().field_Private_APIUser_0;
        }
        public static ApiAvatar GetApiAvatar()
        {
            return GetTarget().field_Private_IUser_0.GetPlayer().prop_ApiAvatar_0;
        }
        #endregion
    }
}
