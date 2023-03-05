package wx;

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

extern class AppTaskBarIconNative extends TaskBarIcon {
    @:native("new AppTaskBarIcon")
    private static function _new():RawPointer<AppTaskBarIconNative>;
    public static inline function createInstance():Pointer<AppTaskBarIconNative> {
        return Pointer.fromRaw(_new());
    }

    @:native("SetPopupMenu")
    public function setPopupMenu(menu:Pointer<Menu>):Void;
}
