import React, { Component, useState } from 'react';
import { Container } from 'reactstrap';
import { Home } from './Home';
import { NavMenu } from './NavMenu';

export const Layout = (props) => {

  const {children} = props

  const [reRenderEmployees, setReRender] = useState(false);
  const [search, setSearch] = useState();

  const reRender = () => {
    setReRender(!reRenderEmployees)
  }

  const updateSearch = (val) => {
    setSearch(val);
  }

    return (
      <div>
        <NavMenu setSearch={updateSearch} reRenderOnClose={reRender} />
        <Container>
            <Home searchValue={search} reRenderEmployees={reRenderEmployees}/>
        </Container>
      </div>
    );
}
