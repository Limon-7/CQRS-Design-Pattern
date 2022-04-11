import React from "react";
import { Button, Container, Menu } from "semantic-ui-react";

interface IProps{
  handleOpenCreateForm:()=>void
}
function Navbar({handleOpenCreateForm}:IProps) {
  const nodeRef= React.useRef(null);
  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item header>
          <img src="/assets/logo.png" alt="logo" style={{marginRight:'10px'}}/>
          Reactivitist
        </Menu.Item>
        <Menu.Item name="Activities" />
        <Menu.Item>
          <Button  content="Create Activity" positive onClick={handleOpenCreateForm}/>
        </Menu.Item>
      </Container>
    </Menu>
  );
}

export default Navbar;
