import React, { Component, useState } from "react";
import {
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";
import { Button, Input, Space } from "antd";
import EmployeeModal from "./modals/employee";

export const NavMenu = ({reRenderOnClose, setSearch}) => {
  const [collapsed, setCollapsed] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const modalType = "add";

  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  };

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    reRenderOnClose();
  };

  const filterData = () => {

  }

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        container
        light
      >
        <NavbarBrand tag={Link} to="/">
          EMS
        </NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          isOpen={collapsed}
          navbar
        >
          <ul className="navbar-nav flex-grow">
            <Space>
            <NavItem>
            <Input placeholder="Search" onChange={(e) => setSearch(e.target.value) }/>
            </NavItem>
            <NavItem>
              <Button type="primary" onClick={showModal}>
                Add Employee
              </Button>
            </NavItem>
            </Space>
            {/* <NavItem>
                <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/emp">Employee</NavLink>
                        </NavItem> */}
          </ul>
        </Collapse>
      </Navbar>
      <EmployeeModal
        isModalOpen={isModalOpen}
        modalType={modalType}
        handleClose={handleCancel}
      />
    </header>
  );
};
