import { User } from './../form/models/user';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../form/services/user.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  providers: [MessageService]
})
export class ListComponent implements OnInit {

  users: User[] = [];
  constructor(
    private userService: UserService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.userService.getAll()
      .then(res => this.users = res)
      .catch(error => console.log(error))
  }

}
