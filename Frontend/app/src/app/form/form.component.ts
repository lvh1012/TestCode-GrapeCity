import { UserService } from './services/user.service';
import { User } from './models/user';
import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  providers: [MessageService]
})
export class FormComponent implements OnInit {

  user: User = {}
  error: any = {
    name: null,
    email: null
  }

  constructor(
    private userService: UserService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {

  }

  save() {
    if (!this.validate()) return;

    this.userService.create(this.user)
      .then(() => {
        alert('Create successful');
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Create successful' });
        this.user = {};
      })
      .catch(error => {
        alert(JSON.stringify(error.error.errors));
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.error.errors });
        console.log(error.error.errors);
      })

  }

  validate() {
    if (!this.validateEmail(this.user.email)) {
      this.error.email = true;
    }
    else {
      this.error.email = false;
    }

    if (!this.user.name) {
      this.error.name = true;
    } else {
      this.error.name = false;
    }

    if (!this.error.name && !this.error.email) return true;
    else return false;
  }

  validateEmail(email?: string) {
    if (!email) return false;
    return email.match(
      /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
  };
}
