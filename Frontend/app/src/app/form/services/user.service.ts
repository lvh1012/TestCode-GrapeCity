import { User } from './../models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable()
export class UserService {
  apiUrl = 'http://localhost:5123/api';
  resource = 'User';
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<any>(`${this.apiUrl}/${this.resource}`)
      .toPromise()
      .then(res => <User[]>res.data)
      .then(data => { return data; });
  }

  create(user: User) {
    return this.http.post<any>(`${this.apiUrl}/${this.resource}`, user)
      .toPromise()
      .then(res => <User>res.data)
      .then(data => { return data; });
  }
}
