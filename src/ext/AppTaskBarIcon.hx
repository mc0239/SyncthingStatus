package ext;

import cpp.Pointer;
import cpp.RawPointer;
import wx.widgets.Menu;
import wx.widgets.TaskBarIcon;

@:buildXml('
<files id="haxe">
    <compilerflag value="-I../include" />
    <file name="../include/AppTaskBarIcon.cpp" />
</files>
')
@:include("AppTaskBarIcon.h")
@:unreflective
@:native("AppTaskBarIcon")
@:structAccess

extern class AppTaskBarIcon extends TaskBarIcon {
    @:native("new AppTaskBarIcon")
    private static function _new():RawPointer<AppTaskBarIcon>;
    public static inline function createInstance():Pointer<AppTaskBarIcon> {
        return Pointer.fromRaw(_new());
    }

    @:native("SetPopupMenu")
    public function setPopupMenu(menu:Pointer<Menu>):Void;
}
