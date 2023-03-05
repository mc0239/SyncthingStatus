#ifndef INCLUDED_AppTaskBarIcon_H
#define INCLUDED_AppTaskBarIcon_H

#include <wx/menu.h>
#include <wx/taskbar.h>

class AppTaskBarIcon : public wxTaskBarIcon {
    private:
        wxMenu* popupMenu;

    public:
        virtual wxMenu *GetPopupMenu() override;
        void SetPopupMenu(wxMenu* menu);
};

#endif
