import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.css']
})
export class AddEditDepComponent implements OnInit {

  constructor(private service: SharedService,private toastr:ToastrService) { }

  @Input() dep: any;
  DepartmentId: string;
  DepartmentName: string;

  ngOnInit(): void {
    this.DepartmentId = this.dep.DepartmentId;
    this.DepartmentName = this.dep.DepartmentName;
  }

  addDepartment() {
    var val = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.service.addDepartment(val).subscribe(result => {
      this.toastr.success(result.toString(),val.DepartmentName);
    });
  }

  updateDepartment() {
    var val = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.service.updateDepartment(val).subscribe(result => {
      this.toastr.info(result.toString(),val.DepartmentName);
    });
  }

}
