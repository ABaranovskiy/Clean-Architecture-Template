import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ShishByzh.Client';

  testGet: string | null = null;
  private _token: string | null = null;

  constructor(private _httpClient: HttpClient) {
  }

  public signIn() {
    console.log('signIn');

    this._httpClient.post<{
      token?: string,
      isSuccessful: boolean,
      errors?: string[]
    }>('/api/authentication/sign-in', {
      username: 'admin',
      password: 'password'
    }, {
      headers: { 'Authorization': `Bearer ${this._token}` }
    }).subscribe(response => {

      console.log(response);

      if (response.isSuccessful && response.token)
        this._token = response.token;
    })
  }

  public getUsers() {
    console.log('getUsers')

    this._httpClient.get('api/user', {
      headers: {
        'Authorization': `Bearer ${this._token}`
      }
    }).subscribe(
      (result) => {
        console.log(result);

        this.testGet = result.toString();
      }

    )
  }

  public testRequest() {
    console.log('testRequest')

    this._httpClient.get('api/user/test', {
      headers: {
        'Authorization': `Bearer ${this._token}`
      }
    }).subscribe(
      (result) => {
        console.log(result);

        this.testGet = result.toString();
      }

    )
  }
}
