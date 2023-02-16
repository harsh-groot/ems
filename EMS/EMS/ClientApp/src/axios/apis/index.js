export const apiUrlBuilder = (url) => {
    return process.env.REACT_APP_API_URL + url
}

export const apiStore = {
    employees:{
        create:apiUrlBuilder('/CreateEmployee'),
        getAll:apiUrlBuilder('/GetEmployees'),
        getEmployeeById: (id) => apiUrlBuilder("/GetEmployeeById/" + id),
        updateEmployeeById: (id) => apiUrlBuilder("/UpdateEmployee/" + id),
        deleteEmployeeById: (id) => apiUrlBuilder("/DeleteEmployee/" + id),
    }
}