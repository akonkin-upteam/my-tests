import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CountryClient {
  constructor(private http: HttpClient) {}

  getCountryData(): Observable<any> {
    return this.http.get(environment.apiUrlCountry + '/countries');
  }
  getProvincesData(countryId:string): Observable<any> {
    return this.http.get(environment.apiUrlCountry + `/provinces?countryId=${countryId}`);
  }

  saveUserData(userId:string, provinceId:string): Observable<any> {    
    const body = {UserId: userId, ProvinceId: provinceId};
    return this.http.post(environment.apiUrlCountry + "/savelocation", body); 
  }
}
