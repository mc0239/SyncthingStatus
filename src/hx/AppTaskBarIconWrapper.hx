package hx;

import cpp.Pointer;
import hx.widgets.Menu;
import hx.widgets.TaskBarIcon;
import wx.AppTaskBarIconNative;

@:access(hx.widgets.Menu)
class AppTaskBarIconWrapper extends TaskBarIcon {
    public function new() {
        if (_ref == null) {
            _ref = AppTaskBarIconNative.createInstance().reinterpret();
        }
        super();
    }

    public function setPopupMenu(menu: Menu): Void {
        coolRef.ptr.setPopupMenu(menu.menuRef);
    }

    private var coolRef(get, null):Pointer<AppTaskBarIconNative>;
    private function get_coolRef():Pointer<AppTaskBarIconNative> {
       return _ref.reinterpret();
    }
}