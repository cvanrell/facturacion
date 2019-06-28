import React from 'react';
import { LanguageSelector } from '../LanguageSelector';

export function LayoutFooter() {
    return (
        <footer>
            <div className="footer-box-left" />
            <div className="footer-box-center">
                <span className="text-muted">
                    Copyright © 2019. Todos los derechos reservados. Sistema desarrollado por <strong>WIS</strong>.
                </span>
            </div>
            <div className="footer-box-right">
                <LanguageSelector />
            </div>
        </footer>
    );
}