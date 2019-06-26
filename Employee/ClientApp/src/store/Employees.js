const requestAllEmployeesType = 'REQUEST_ALL_EMPLOYEES';
const receiveAllEmployeesType = 'RECEIVE_ALL_EMPLOYEES';
const receiveAllActiveEmployeesType = 'RECEIVE_ALL_ACTIVE_EMPLOYEES';
const addNewEmployee = 'ADD_EMPLOYEE';
const changeStatusType = 'CHANGE_STATUS';
const errorType = 'ERROR';
const initialState = { employees: [], isLoading: false, success: false, message: null, getAll: true };

export const actionCreators = {
    createNewEmployee: (newEmployee) => async (dispatch, getState) => {
        console.log(newEmployee);
        const formData = new FormData();
        formData.append('employeeId', newEmployee.employeeId);
        formData.append('firstName', newEmployee.firstName);
        formData.append('lastName', newEmployee.lastName);
        formData.append('status', newEmployee.status);

        const response = await fetch(`api/Employee/Create`, {
            method: 'PUT',
            body: formData
        });
        if (response.ok) {
            const employee = await response.json();
            dispatch({ type: addNewEmployee, employee });
        }
        else {
            const text = await response.text();
            dispatch({ type: errorType, errorMessage: JSON.parse(text).message });
        }
    },
    changeStatus: (employeeId) => async (dispatch, getState) => {
        console.log(employeeId);

        const response = await fetch(`api/Employee/ChangeEmployeeStatus?id=${employeeId}`, {
            method: 'POST'
        });
        if (response.ok) {
            const employee = await response.json();
            dispatch({ type: changeStatusType, employee });
        }
        else {
            const text = await response.text();
            dispatch({ type: errorType, errorMessage: JSON.parse(text).message });
        }
    },
    requestAllEmployees: () => async (dispatch, getState) => {
        console.log(getState());
        if (getState().employee.isLoading) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestAllEmployeesType });

        const url = `api/Employee/GetAllEmployees`;
        const response = await fetch(url);
        const employees = await response.json();

        dispatch({ type: receiveAllEmployeesType, employees });
    },
    requestAllActiveEmployees: () => async (dispatch, getState) => {
        console.log(getState());
        if (getState().employee.isLoading) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: requestAllEmployeesType });

        const url = `api/Employee/GetAllActiveEmployees`;
        const response = await fetch(url);
        const employees = await response.json();

        dispatch({ type: receiveAllActiveEmployeesType, employees });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestAllEmployeesType) {
        return {
            ...state,
            employees: action.employees,
            isLoading: true,
            success: true,
            message: null
        };
    }

    if (action.type === receiveAllEmployeesType) {
        return {
            ...state,
            employees: action.employees,
            isLoading: false,
            getAll: true,
            success: true,
            message: null
        };
    }

    if (action.type === receiveAllActiveEmployeesType) {
        return {
            ...state,
            employees: action.employees,
            isLoading: false,
            getAll: false,
            success: true,
            message: null
        };
    }

    if (action.type === addNewEmployee) {
        return {
            ...state,
            employees: [
                ...state.employees,
                {
                    employeeId: action.employee.employeeId,
                    firstName: action.employee.firstName,
                    lastName: action.employee.lastName,
                    status: action.employee.status
                }
            ],
            message: 'Successfully created new employee.',
            success: true
        };
    }

    if (action.type === changeStatusType) {
        return {
            ...state,
            employees: state.employees.map(
                (employee, i) => employee.employeeId === action.employee.employeeId ? { ...employee, status: action.employee.status }
                    : employee
            ),
            message: 'Successfully changed status of employee.',
            success: true
        };
    }

    if (action.type === errorType) {
        return {
            ...state,
            message: action.errorMessage,
            success: false
        };
    }

    return state;
};
