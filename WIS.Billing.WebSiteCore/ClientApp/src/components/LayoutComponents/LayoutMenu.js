import React, { useState } from 'react';
import { LayoutMenuSection } from './LayoutMenuSection';
import { LayoutMenuHeader } from './LayoutMenuHeader';

export function LayoutMenu(props) {
    const sectionsPrueba = [
        {
            id: "LSTO000_Section_Access_Stock",
            label: "Stock",
            icon: "fa fa-cubes",
            items: [
                {
                    id: "WSTO395_Page_Access_StockProducto",
                    label: "Stock por producto",
                    type: "action",
                    url: "/stock/STO395",
                    module: "base"
                },
                {
                    id: "WSTO150_Page_Access_StockUbicacion",
                    label: "Stock por ubicación",
                    type: "action",
                    url: "/stock/STO150",
                    module: "base"
                }
            ]
        },
        {
            id: "DOC000_Section_Access_ControlDocumental",
            label: "Control documental",
            icon: "fa fa-file",
            items: [
                {
                    id: "DOC080_Page_Access_OrdenesIngreso",
                    label: "Órdenes de Ingreso",
                    type: "action",
                    url: "/documento/DOC080",
                    module: "base"
                },
                 {
                     id: "DOC260_Page_Access_CambioEstado",
                     label: "Cambio de Estado",
                     type: "action",
                     url: "/documento/DOC260",
                     module: "base"
                 }
            ]
        }];

    const [menuOpen, setOpen] = useState(false);
    const [sections, setSections] = useState(sectionsPrueba);
    const [searchValue, setSearchValue] = useState("");

    function handleTransitionEnd() {
        console.log("ameno");
        props.setMenuOpening(false);
    }

    function updateSearch(value) {
        setSearchValue(value);
    }
    function openMenu() {
        setOpen(true);
    }
    function closeMenu() {
        setSearchValue("");
        setOpen(false);
    }

    const menuSections = sections.map(s => (
        <LayoutMenuSection
            key={s.id}
            id={s.id}
            label={s.label}
            icon={s.icon}
            items={s.items}
            searchValue={searchValue}
            menuOpen={menuOpen}
        />
    ));

    const menuClass = menuOpen ? "wis-nav-open" : "wis-nav-closed";

    return (
        <nav className={menuClass} onTransitionEnd={handleTransitionEnd}>
            <div className="wis-nav-container">
                <LayoutMenuHeader
                    updateSearch={updateSearch}
                    openMenu={openMenu}
                    closeMenu={closeMenu}
                    menuOpen={menuOpen}
                />
                <div className="wis-menu">
                    {menuSections}
                </div>
            </div>
        </nav>
    );
}