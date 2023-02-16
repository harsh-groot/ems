import {
    Button,
    Col,
    Form,
    Input,
    Modal,
    Row,
    DatePicker,
    message,
} from "antd";
import React, { useEffect } from "react";
import { apiStore } from "../../axios/apis";
import { getApiService } from "../../axios/services/get.service";
import { postApiService } from "../../axios/services/post.service";
import { putApiService } from "../../axios/services/put.service";
import moment from "moment";
import dayjs from "dayjs"

const EmployeeModal = ({ isModalOpen, modalType, handleClose, empId }) => {
    const [form] = Form.useForm();

    const onFinish = (values) => {
        let payload = {
            id: empId ?? 0,
            name: values?.name,
            email: values?.email,
            department: values?.department,
            dateOfBirth: values.dateOfBirth
        }

        if (modalType === "add") {
            postApiService(apiStore.employees.create, payload).then((res) => {
                if (res.status === 200) {
                    message.success(res?.data?.responseMessage);
                    handleClose();
                } else {
                    message.error(res?.response?.data?.responseMessage)
                }
            })
        } else {
            putApiService(apiStore.employees.updateEmployeeById(empId), payload).then(
                (res) => {
                    if (res.status === 200) {
                        message.success(res?.data?.responseMessage);
                        handleClose();
                    }
                }
            );
        }
    };

    const onFinishFailed = (errorInfo) => {
        console.log("Failed:", errorInfo);
    };

    const getEmployeeDetails = () => {
        getApiService(apiStore.employees.getEmployeeById(empId)).then((res) => {
            form.setFieldValue("name", res.name);
            form.setFieldValue("email", res.email);
            form.setFieldValue("department", res.department);
            if (res.dateOfBirth != null)
                form.setFieldValue('dateOfBirth', dayjs(moment(res.dateOfBirth).format('YYYY-MM-DD'), 'YYYY-MM-DD'));
        });
    };

    useEffect(() => {
        form.resetFields();
    }, [isModalOpen]);

    if (empId) {
        getEmployeeDetails();
    }

    return (
        <Modal
            title={(modalType === "add" ? "Add " : "Edit ") + " Employee"}
            open={isModalOpen}
            onCancel={handleClose}
            footer={null}
        >
            <Form
                form={form}
                name="basic"
                autocomplete="off"
                labelCol={{ span: 0 }}
                wrapperCol={{ span: 24 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
            >
                <Form.Item
                    label="Name"
                    name="name"
                    labelCol={{ span: 6 }}
                    rules={[{ required: true, message: "Please input your name!" }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Email"
                    name="email"
                    labelCol={{ span: 6 }}
                    rules={[{ required: true, message: "Please input your email!" }]}
                >
                    <Input type="email" />
                </Form.Item>

                <Form.Item
                    label="Department"
                    name="department"
                    labelCol={{ span: 6 }}
                    rules={[{ required: true, message: "Please input your department!" }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item label="DOB" labelCol={{ span: 6 }} name="dateOfBirth">
                    <DatePicker style={{ width: "100%" }} />
                </Form.Item>

                <Row gutter={16} className="justify-content-center">
                    <Col className="gutter-row" labelCol={{ span: 6 }}>
                        <Button key="submit" type="primary" htmlType="submit">
                            {modalType === "add" ? "Submit" : "Update"}
                        </Button>
                    </Col>
                    <Col className="gutter-row" labelCol={{ span: 6 }}>
                        <Button key="submit" type="default" onClick={handleClose}>
                            Cancel
                        </Button>
                    </Col>
                </Row>
            </Form>
        </Modal>
    );
};

export default EmployeeModal;
