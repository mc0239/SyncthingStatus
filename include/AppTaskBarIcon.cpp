#ifndef INCLUDED_AppTaskBarIcon
#define INCLUDED_AppTaskBarIcon

#include <wx/menu.h>
#include <wx/taskbar.h>

#include "AppTaskBarIcon.h"

wxMenu *AppTaskBarIcon::GetPopupMenu() {
    return this->popupMenu;
}

void AppTaskBarIcon::SetPopupMenu(wxMenu* menu) {
    this->popupMenu = menu;
}

#endif