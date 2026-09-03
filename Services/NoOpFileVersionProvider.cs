using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmpManagementSystem.Services;

// The default IFileVersionProvider watches static files (CSS/JS) using a
// FileSystemWatcher so it can add a cache-busting "?v=..." to their URLs.
// That watcher hits the same inotify-instance limit we fixed for config
// reload earlier — every <link>/<script> tag creates its own watcher.
// This version just skips that feature entirely (returns the path
// unchanged), which is fine for a small demo site with no cache-busting
// needs.
public class NoOpFileVersionProvider : IFileVersionProvider
{
    public string AddFileVersionToPath(PathString requestPathBase, string path) => path;
}
