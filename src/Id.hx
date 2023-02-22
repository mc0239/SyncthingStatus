package ;

import hx.widgets.StandardId;

class Id {
    public static final OPEN_WEB: Int                   = StandardId.HIGHEST + 1;
    public static final SAVE_SETTINGS_BUTTON_RESET: Int = StandardId.HIGHEST + 2;
    
    // Just assuming there will be at most 100 folders.
    public static final OPEN_FOLDER_START: Int = StandardId.HIGHEST + 100;
    public static final OPEN_FOLDER_END: Int   = StandardId.HIGHEST + 100 + 100;
}