using ExampleTextMenu.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace ExampleTextMenu.Controller
{
    class ControllerMenu<T> where T : IMenuItem
    {
        public static readonly string id_exit = "exit";

        private static ControllerMenu<T> instance;

        public static ControllerMenu<T> Instance
        {
            get
            {
                if (instance == null) instance = new ControllerMenu<T>();
                return instance;
            }
        }

        private const string path_data_cache = "\\Content\\menu.json";

        private MenuItem<T> menu;
        private MenuItem<T> currentMenu;

        private int selectedMenuItemIndex;
        private static readonly string back_text = "back";

        public int SelectedMenuItemIndex { get { return selectedMenuItemIndex; } }

        public string CurrentMenuTitle
        {
            get
            {
                return currentMenu == null || currentMenu.Data == null ? null : currentMenu.Data.getDisplayText();
            }
        }

        public int CurrentMenuItemsCount
        {
            get
            {
                return currentMenu == null || currentMenu.Childs == null
                    ? 0
                    : currentMenu.Childs.Count + (currentMenu.Parent == null ? 0 : 1);
            }
        }

        internal string CurrentMenuItemTitle(int position)
        {
            if (CurrentMenuItemsCount <= position) return null;

            return currentMenu.Childs.Count <= position
                ? back_text
                : currentMenu.Childs[position].Data.getDisplayText();
        }

        public bool IsCurrentMenuExist
        {
            get
            {
                return currentMenu != null;
            }
        }

        public void init()
        {
            menu = MenuItem<T>.deserealize(null, readJson());
            currentMenu = menu;
            selectedMenuItemIndex = 0;
        }

        private string readJson()
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return File.ReadAllText(path + path_data_cache);
        }

        internal void navigateUp()
        {
            if (currentMenu == null) return;
            currentMenu = currentMenu.Parent;
            selectedMenuItemIndex = 0;
        }

        internal void navigateInto()
        {
            if (CurrentMenuItemsCount <= selectedMenuItemIndex)
                return;

            if (selectedMenuItemIndex == currentMenu.Childs.Count)
                navigateUp();
            else
            {
                currentMenu = currentMenu.Childs[selectedMenuItemIndex];
                selectedMenuItemIndex = 0;
            }
        }

        internal void navigateInto(int i)
        {
            selectedMenuItemIndex = i;
            navigateInto();
        }

        internal void setNextSelectedMenuItemIndex()
        {
            if (CurrentMenuItemsCount == 0) return;
            ++selectedMenuItemIndex;
            selectedMenuItemIndex %= CurrentMenuItemsCount;
        }

        internal void setPreviousSelectedMenuItemIndex()
        {
            if (CurrentMenuItemsCount == 0) return;
            selectedMenuItemIndex = (CurrentMenuItemsCount + selectedMenuItemIndex - 1) % CurrentMenuItemsCount;
        }
    }
}
