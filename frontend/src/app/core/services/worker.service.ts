import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';
import {ApiResponse} from '../../shared/models/api-response.model';

@Injectable({ providedIn: 'root' })
export class WorkerService {

  private api = environment.apiUrl + '/Schedules';

  constructor(private http: HttpClient) {}

  submitSchedule(dto: any) {
    return this.http.post<ApiResponse<boolean>>(`${this.api}/submit`, dto);
  }

  getMySchedules() {
    return this.http.get<ApiResponse<any[]>>(`${this.api}/my-schedules`);
  }

  getShiftTypes() {
    return this.http.get<ApiResponse<any[]>>(`${this.api}/shift-types`);
  }
}
