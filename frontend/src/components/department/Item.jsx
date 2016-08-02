import React from 'react'

const Item = (props) => {
    const department = props.element.department;
    const departmentManager = props.element.departmentManager;
    const employeeNo = props.element.employee;
    const projectNo = props.element.project;
    const onEdit = props.onEdit;
    const onViewEmployee = props.onViewEmployee;
    const onViewProjects = props.onViewProjects;

   return(
       <div>
           
       </div>
       // <tr>
       //     <td>{department}</td>
       //     <td>{departmentManager}</td>
       //     <td>{employeeNo}</td>
       //     <td>{projectNo}</td>
       //     <td><div className="btn-wrapper"><button className="btn btn-xs btn-info" id="btn"
       //                                              onClick={ onEdit }>Edit</button></div></td>
       //
       // </tr>
   )};


export default Item;