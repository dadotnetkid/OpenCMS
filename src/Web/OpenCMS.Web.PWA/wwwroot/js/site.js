
function collapseSideBar() {
    var bodyClass = window.document.body.classList;
    var collapsed = bodyClass.contains("sidebar-collapse");
    if (collapsed) {
        bodyClass.remove("sidebar-collapse");
    } else {
        bodyClass.add("sidebar-collapse");
    }
}