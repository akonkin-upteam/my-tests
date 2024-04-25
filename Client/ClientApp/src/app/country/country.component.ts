import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CountryClient } from '../clients/country.client';
import { AlertService } from '../services/alert.service';

interface IdValuePair {
  id: string;
  value: string;
}


@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css'],
})
export class CountryComponent implements OnInit {
  public countryForm!: FormGroup;

  public countries: IdValuePair[] = []
  public provincies: IdValuePair[] = []

  constructor(
    private authenticationService: AuthenticationService,    
    private countryClient: CountryClient,    
    private alertService: AlertService
  ) {}

  private currentProvinceId: string = '';

  ngOnInit() {
    this.countryForm = new FormGroup({
      country: new FormControl('', Validators.required),
      province: new FormControl('', [Validators.required, Validators.email]),
    });
    this.countryClient.getCountryData().subscribe((res) => {      
        this.countries = res.map((s: { id: any; name: any; }) => {
          return { id: s.id, value: s.name };
        });
      },
      (err) => {
        this.alertService.openSnackBar(`Server error: ${err.error}`);
      }
    );
  }

  logout(): void {
    this.authenticationService.logout();
  }

  public onChangeCountry(event:any) {
    this.currentProvinceId = '';
    this.provincies = [];
    this.countryClient.getProvincesData(event.value).subscribe((res) => {      
      this.provincies = res.map((s: { id: any; name: any; }) => {
        return { id: s.id, value: s.name };
      });
    },
    (err) => {
      this.alertService.openSnackBar(`Server error: ${err.error}`);
    }
  );
  }

  public onChangeProvince(event:any) {
    this.currentProvinceId = event.value;
  }

  public onSubmit() {
    if (this.currentProvinceId === '') {
      this.alertService.openSnackBar(`Error: Province is not selected`);
    }      
    this.authenticationService.getCurrentUserInfo().subscribe((res) => {      
      this.countryClient.saveUserData(res[0].id, this.currentProvinceId).subscribe((resp) => {          
          this.alertService.openSnackBar(`Saved location for user ${resp.userId}`);
        },
        (err) => {
          this.alertService.openSnackBar(`Save location error: ${err.error}`);
        }
      );
    }); 
  }

}
