import React from 'react';
import ReactDOM from 'react-dom';
import { Button } from "@material-ui/core";
import Menu from "../Shared/Menu";
import { HashRouter } from 'react-router-dom';

if (module.hot) {
    module.hot.accept();
}
function App() {
    return <div>
        <Menu />
        <Button variant="flat">Sfsd v</Button>
    </div>;
}

ReactDOM.render((
    <HashRouter>
        <App />
    </HashRouter>
), document.getElementById('app'));