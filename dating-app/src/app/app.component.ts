import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AppUser } from './_models/app-user';
 
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  name = 'AnTM';
  users: AppUser[] = [];
 
  constructor(private httpClient: HttpClient) {
  }
 
  ngOnInit(): void {
    this.httpClient.get<AppUser[]>("https://localhost:7069/api/Auth")
      .subscribe(
        response => this.users = response,
        error => console.log(error));
  }
}


