import React, { useState, useEffect } from "react";
import { Button, message, Popconfirm, Space, Table } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";
import EmployeeModal from "./modals/employee";
import { getApiService } from "../axios/services/get.service";
import { apiStore } from "../axios/apis";
import { deleteApiService } from "../axios/services/delete.service";
import moment from "moment";

export const Home = ({ reRenderEmployees, searchValue }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [employeeId, setEmployeeId] = useState(null);
    const [dataCopy, setDataCopy] = useState([]);

    const modalType = "edit";

    const showModal = () => {
        setIsModalOpen(true);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
        getEmployees();
    };

    const confirm = (id) => {
        deleteApiService(apiStore.employees.deleteEmployeeById(id)).then((res) => {
            if (res.status === 200) {
                message.success(res?.data?.responseMessage);
                getEmployees();
            }
        });
    };

    const editEmployee = (id) => {
        setEmployeeId(id);
        showModal();
    };

    const [dataSource, setDataSource] = useState([]);

    const columns = [
        {
            title: "Name",
            dataIndex: "name",
            key: "name",
        },
        {
            title: "Email",
            dataIndex: "email",
            key: "email",
        },
        {
            title: "Date of Birth",
            key: "dateOfBirth",
            render: (record) => {
                return <span>{record?.dateOfBirth != null ? moment(record?.dateOfBirth).format('DD-MM-YYYY') : undefined}</span>
            }
        },
        {
            title: "Department",
            dataIndex: "department",
            key: "department",
        },
        {
            title: "Actions",
            key: "actions",
            render: (record) => (
                <Space size="middle">
                    <EditOutlined
                        style={{ cursor: "pointer", color: "blue" }}
                        onClick={() => editEmployee(record?.id)}
                    />
                    <Popconfirm
                        title="Delete Employee"
                        description="Are you sure, you want to delete this employee?"
                        onConfirm={() => confirm(record?.id)}
                        onOpenChange={() => console.log("open change")}
                    >
                        <DeleteOutlined style={{ cursor: "pointer", color: "red" }} />
                    </Popconfirm>
                </Space>
            ),
        },
    ];

    const getEmployees = () => {
        getApiService(apiStore.employees.getAll).then((res) => {
            if (res?.length) {
                setDataSource(res);
                setDataCopy(res);
            } else {
                setDataSource([]);
            }
        });
    };

    useEffect(() => {
        if (searchValue !== null && searchValue !== undefined && searchValue !== '') {
            let filterData = dataCopy?.length && dataCopy.filter((item) =>
                ((item?.name)?.toLowerCase())?.includes((searchValue)?.toLowerCase()) ||
                ((item?.email)?.toLowerCase())?.includes((searchValue)?.toLowerCase()) ||
                ((item?.department)?.toLowerCase())?.includes((searchValue)?.toLowerCase())
            )

            setDataSource(filterData)
        } else {
            getEmployees()
        }
    }, [searchValue, reRenderEmployees])

    return (
        <div>
            <Table dataSource={dataSource} columns={columns} />
            {isModalOpen && <EmployeeModal
                isModalOpen={isModalOpen}
                modalType={modalType}
                handleClose={handleCancel}
                empId={employeeId}
            />}
        </div>
    );
};
