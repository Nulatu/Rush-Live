# Rush-Live

In complex objects with many children, I try not to use - `GetChild` and `transform.parent`, `GetSiblingIndex`. 
Otherwise, it will be difficult for Senior to modify the hierarchy of objects in the scene.
I do not use the `Find` function of Unity.
The maximum number of lines in the script is ~ 200-250.

**Encapsulation** :
1) Open Data - Uppercase Variable Name
2) Private data - Lowercase variable name
3) I declare open data on top (links to other scripts), closed data on the bottom. Like a bookmarked book.
4) If the script variable is initialized in the inspector and it has no links with other scripts. 
Instead of `public` I use `[SerializField] private`.
5) When a script variable refers to another script but is not initialized in its inspector.
Instead of `public` I use `[HideInspector] public`.
6) To strictly separate public and private data, I write explicitly everywhere default access modifiers.
Instead of `void NameFunction()` I use `private void NameFunction()`.
