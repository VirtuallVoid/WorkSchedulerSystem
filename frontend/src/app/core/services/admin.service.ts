import {map, Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Injectable} from '@angular/core';
import {Schedule} from '../../shared/models/schedule.model';
import {ApiResponse} from '../../shared/models/api-response.model';

@Injectable({ providedIn: 'root' })
export class AdminService {

  private api = environment.apiUrl + '/Schedules';

  constructor(private http: HttpClient) {}

  getAllSchedules(): Observable<Schedule[]> {
    return this.http
      .get<ApiResponse<Schedule[]>>(`${this.api}/all`)
      .pipe(map(r => r.data));
  }

  approve(id: number) {
    return this.http.put<ApiResponse<any[]>>(`${this.api}/approve`, { scheduleId: id });
  }

  reject(id: number) {
    return this.http.put<ApiResponse<any[]>>(`${this.api}/reject`, { scheduleId: id });
  }
}
