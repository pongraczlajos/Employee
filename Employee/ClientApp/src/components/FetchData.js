import React, { Component } from 'react';
import { Alert, Button, ButtonGroup, ButtonToolbar, Modal, ModalHeader, ModalFooter, ModalBody, Form, FormGroup, Label, Input } from 'reactstrap';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Employees';

class FetchData extends Component {
    constructor(props) {
        super(props);
        this.state = {
            show: false,
            valid: false,
            employeeId: '',
            firstName: '',
            lastName: '',
            active: false
        };

        this.getAll = this.getAll.bind(this);
        this.getAllActive = this.getAllActive.bind(this);
        this.openCreateEmployee = this.openCreateEmployee.bind(this);
        this.closeCreateEmployee = this.closeCreateEmployee.bind(this);
        this.createEmployee = this.createEmployee.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.changeStatus = this.changeStatus.bind(this);
        this.renderEmployeesTable = this.renderEmployeesTable.bind(this);
    }

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.getAll();
    }

    componentDidUpdate() {
        // This method is called when the route parameters change
        //this.getAll();
    }

    getAll() {
        this.props.requestAllEmployees();
    }

    getAllActive() {
        this.props.requestAllActiveEmployees();
    }

    changeStatus(employeeId) {
        this.props.changeStatus(employeeId);
    }

    closeCreateEmployee() {
        this.setState({
            show: false,
            employeeId: '',
            firstName: '',
            lastName: '',
            active: false,
            valid: false
        });
    }

    openCreateEmployee() {
        this.setState({ show: true });
    }

    createEmployee() {
        this.props.createNewEmployee({
            employeeId: this.state.employeeId,
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            status: this.state.active,
        });
        this.closeCreateEmployee();
    }

    validateNewEmployee(state) {
        return state.employeeId !== '' && state.firstName !== '' && state.lastName !== '';
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
        this.setState({
            valid: this.validateNewEmployee(this.state)
        });
    }

    renderEmployeesTable() {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Employee ID</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.employees ? this.props.employees.map(employee =>
                        <tr key={employee.employeeId}>
                            <td>{employee.employeeId}</td>
                            <td>{employee.firstName}</td>
                            <td>{employee.lastName}</td>
                            <td>{employee.status ? 'Active' : 'Inactive'}</td>
                            <td><Button disabled={!this.props.getAll} onClick={() => this.changeStatus(employee.employeeId)}>{employee.status ? 'Make Inactive' : 'Make Active'}</Button></td>
                        </tr>
                    ) : []}
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div>
                <h1>Employee</h1>
                <div style={{ 'marginBottom': '10px' }}>
                    <ButtonToolbar className="justify-content-between">
                        <ButtonGroup>
                            <Button onClick={this.getAll}>All</Button>
                            <Button onClick={this.getAllActive}>Active</Button>
                        </ButtonGroup>
                        <ButtonGroup>
                            <Button disabled={!this.props.getAll} onClick={this.openCreateEmployee}>Add</Button>
                        </ButtonGroup>
                    </ButtonToolbar>
                </div>
                {this.props.message ? this.props.success ? <Alert color='success'>{this.props.message}</Alert> : <Alert color='danger'>{this.props.message}</Alert> : []}
                <Modal isOpen={this.state.show}>
                    <ModalHeader>Create new Employee</ModalHeader>
                    <ModalBody>
                        <Form>
                            <FormGroup>
                                <Label for="exampleEmail">Employee ID</Label>
                                <Input type="text" name="employeeId" id="employeeId" placeholder="Employee ID"
                                    value={this.state.employeeId} onChange={this.handleInputChange} />
                            </FormGroup>
                            <FormGroup>
                                <Label for="exampleEmail">First Name</Label>
                                <Input type="text" name="firstName" id="firstName" placeholder="First Name"
                                    value={this.state.firstName} onChange={this.handleInputChange} />
                            </FormGroup>
                            <FormGroup>
                                <Label for="exampleEmail">Last Name</Label>
                                <Input type="text" name="lastName" id="lastName" placeholder="Last Name"
                                    value={this.state.lastName} onChange={this.handleInputChange} />
                            </FormGroup>
                            <FormGroup check>
                                <Label check><Input name="active" id="active" type="checkbox"
                                    value={this.state.active} onChange={this.handleInputChange} />Active</Label>
                            </FormGroup>
                        </Form>
                    </ModalBody>
                    <ModalFooter>
                        <Button variant="secondary" onClick={this.closeCreateEmployee}>Close</Button>
                        <Button variant="primary" onClick={this.createEmployee} disabled={!this.state.valid}>Create</Button>
                    </ModalFooter>
                </Modal>
                {this.renderEmployeesTable()}
            </div>
        );
    }
}

export default connect(
  state => state.employee,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(FetchData);
