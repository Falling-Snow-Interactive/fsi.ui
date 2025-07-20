using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsi.Ui.Menu
{
    public class FsiMenu<T> : MonoBehaviour
        where T : Enum
    {
        private FsiPage<T> currentPage;
        public FsiPage<T> CurrentPage => currentPage;

        [Header("Fsi Menu")]

        [SerializeField]
        private bool openOnStart = true;

        [Header("Pages")]

        [SerializeField]
        private List<FsiPage<T>> pages = new();

        private Dictionary<T, FsiPage<T>> cache = null;
        private Dictionary<T, FsiPage<T>> Pages
        {
            get
            {
                cache ??= BuildDictionary();
                return cache;
            }
        }

        [Header("References")]

        [SerializeField]
        private GameObject root;

        private void Awake()
        {
            foreach (FsiPage<T> page in pages)
            {
                page.gameObject.SetActive(true);
                page.Close();
            }
        }

        protected virtual void Start()
        {
            foreach (FsiPage<T> page in pages)
            {
                page?.Initialize(this);
            }

            if (openOnStart)
            {
                Open();
            }
        }

        public virtual void Open()
        {
            root?.SetActive(true);
            GoToPage(default(T));
        }

        public virtual void Close()
        {
            root?.SetActive(false);
        }

        public void GoToPage(T to)
        {
            CurrentPage?.Close();
            if (!Pages.TryGetValue(to, out currentPage))
            {
                Debug.Log($"Menu ({name}) could not find page of type {to}.", gameObject);
            }
            CurrentPage?.Open();
        }

        private Dictionary<T, FsiPage<T>> BuildDictionary()
        {
            string s = "";
            
            Dictionary<T, FsiPage<T>> dict = new();
            for (int i = 0; i < pages.Count; i++)
            {
                FsiPage<T> page = pages[i];
                dict.Add(page.Page, page);
                s += $"Adding Page {page.Page}";
                if (i < pages.Count - 1)
                {
                    s += "\n";
                }
            }
            
            Debug.Log($"Menu ({name}) indexed {pages.Count} pages.\n" +
                      $"{s}", gameObject);
            return dict;
        }
    }
}