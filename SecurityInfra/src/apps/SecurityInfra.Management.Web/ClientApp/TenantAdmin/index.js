import React from 'react';
import ReactDOM from 'react-dom';
import { Button } from "@material-ui/core";
import Header from './Header';


if (module.hot) {
    module.hot.accept();
}
function App() {
    return <div>
        <Header />
        <Button variant="flat">Sfsd gf</Button>
    </div>;
}
ReactDOM.render(<App />, document.getElementById('app'));