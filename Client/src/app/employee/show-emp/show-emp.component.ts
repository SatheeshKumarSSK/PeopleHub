import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service:SharedService,private toastr:ToastrService) { }

  EmployeeList:any=[];

  ModalTitle:string;
  ActivateAddEditEmpComp:boolean=false;
  employee:any;

  EmployeeIdFilter: string = "";
  EmployeeNameFilter: string = "";
  EmployeeListWithoutFilter: any = [];

  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick(){
    this.employee={
      EmployeeId:0,
      EmployeeName:"",
      Department:"",
      DateOfJoining:"",
      PhotoFileName:"anonymous.png"
    }
    this.ModalTitle="Add Employee";
    this.ActivateAddEditEmpComp=true;

  }

  editClick(item){
    this.employee=item;
    this.ModalTitle="Edit Employee";
    this.ActivateAddEditEmpComp=true;
  }

  deleteClick(item){
    if(confirm('Are you sure to delete '+item.EmployeeName+'?' )){
      this.service.deleteEmployee(item.EmployeeId).subscribe(data=>{
        this.refreshEmpList();
        this.toastr.error(data.toString(),item.EmployeeName);
      })
    }
  }

  closeClick(){
    this.ActivateAddEditEmpComp=false;
    this.refreshEmpList();
  }


  refreshEmpList(){
    this.service.getEmpList().subscribe(data=>{
      this.EmployeeList=data;
      this.EmployeeListWithoutFilter=data;
    });
  }

  FilterFn() {
    var EmployeeIdFilter = this.EmployeeIdFilter;
    var EmployeeNameFilter = this.EmployeeNameFilter;

    this.EmployeeList = this.EmployeeListWithoutFilter.filter(function (el) {
      return el.EmployeeId.toString().toLowerCase().includes(
        EmployeeIdFilter.toString().trim().toLowerCase()
      ) &&
        el.EmployeeName.toString().toLowerCase().includes(
          EmployeeNameFilter.toString().toLowerCase()
        )
    });
  }

  sortResult(prop, asc) {
    this.EmployeeList = this.EmployeeListWithoutFilter.sort(function (a, b) {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }

}
