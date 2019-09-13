import React, { useState, useEffect } from "react";
import IconButton from "@material-ui/core/IconButton";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";
import MoreVertIcon from "@material-ui/icons/MoreVert";

const m = [
    {
        "url": "http://localhost",
        "text": "None b vcbhvcnbcvnjbvnbvnbvmbv bv mv"
    },
    {
        "url": "http://localhost2",
        "text": "None b vcbhvcnb"
    }
];

export default () => {
    useEffect(() => {

    }, []);
    const [menuItems, setMenuItems] = useState(m);
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    function handleClick(event) {
        setAnchorEl(event.currentTarget);
    }

    function handleClose() {
        setAnchorEl(null);
    }

    function handleRedirect(url) {
        window.location.href = url;
    }

    return (
        <div>
            <IconButton
                aria-label="more"
                aria-controls="long-menu"
                aria-haspopup="true"
                color="inherit"
                onClick={handleClick}
            >
                <MoreVertIcon />
            </IconButton>
            <Menu
                id="long-menu"
                anchorEl={anchorEl}
                keepMounted
                open={open}
                onClose={handleClose}
            >
                {menuItems.map(item => (
                    <MenuItem
                        key={item.url}
                        selected={item.url === window.location.pathname}
                        onClick={() => handleRedirect(item.url)}
                    >
                        {item.text}
                    </MenuItem>
                ))}
            </Menu>
        </div>
    );
};
