package ext;

import cpp.Pointer;
import ext.AppTaskBarIcon;
import hx.widgets.Menu;
import hx.widgets.TaskBarIcon;

@:access(hx.widgets.Menu)
// Can't be named same as cpp file (AppTaskBarIcon) because there's a weird name clash during compiling/linking.
class AppTaskBarIconW extends TaskBarIcon {
    public function new() {
        if (_ref == null) {
            _ref = AppTaskBarIcon.createInstance().reinterpret();
        }
        super();
    }

    public function setPopupMenu(menu: Menu): Void {
        coolRef.ptr.setPopupMenu(menu.menuRef);
    }

    private var coolRef(get, null):Pointer<AppTaskBarIcon>;
    private function get_coolRef():Pointer<AppTaskBarIcon> {
       return _ref.reinterpret();
    }
}