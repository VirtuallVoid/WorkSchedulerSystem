import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AdminService } from '../../../core/services/admin.service';
import { Schedule } from '../../../shared/models/schedule.model';
import { DatePipe, NgClass, CommonModule } from '@angular/common';
import {LogoutButtonComponent} from '../../../shared/logout-button/logout-button.component';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.page.html',
  imports: [
    DatePipe,
    NgClass,
    CommonModule,
    LogoutButtonComponent
  ],
  styleUrls: ['./admin-dashboard.page.scss']
})
export class AdminPage implements OnInit {

  schedules: Schedule[] = [];
  filtered: Schedule[] = [];
  jobs: string[] = [];
  days: Date[] = [];

  weekStart!: Date;
  weekEnd!: Date;
  weekNumber!: number;

  constructor(private adminService: AdminService, private cdr: ChangeDetectorRef) {}

  async ngOnInit() {
    this.initializeCurrentWeek();
    await this.loadSchedules();
  }

  async loadSchedules() {
    const res = await firstValueFrom(this.adminService.getAllSchedules());
    this.schedules = res || [];

    this.jobs = [...new Set(
      this.schedules.map(x => x.jobName ?? 'Unknown Job')
    )];
    this.applyFilter();
    this.cdr.detectChanges();
  }

  applyFilter() {
    this.filtered = this.schedules.filter(s => {
      const shift = new Date(s.shiftDate);
      shift.setHours(0, 0, 0, 0);
      const start = new Date(this.weekStart);
      start.setHours(0, 0, 0, 0);
      const end = new Date(this.weekEnd);
      end.setHours(0, 0, 0, 0);
      return shift >= start && shift <= end;
    });
  }

  initializeCurrentWeek() {
    const today = new Date();
    this.setWeek(today);
  }

  setWeek(date: Date) {
    const day = date.getDay();
    const diffToMonday = day === 0 ? -6 : 1 - day;

    this.weekStart = new Date(date);
    this.weekStart.setDate(date.getDate() + diffToMonday);

    this.weekEnd = new Date(this.weekStart);
    this.weekEnd.setDate(this.weekStart.getDate() + 6);

    this.days = [];
    for (let i = 0; i < 7; i++) {
      const d = new Date(this.weekStart);
      d.setDate(this.weekStart.getDate() + i);
      this.days.push(d);
    }

    this.weekNumber = this.getWeekNumber(this.weekStart);
  }

  getWeekNumber(date: Date) {
    const firstDay = new Date(date.getFullYear(), 0, 1);
    const pastDays = Math.floor((+date - +firstDay) / 86400000);
    return Math.ceil((pastDays + firstDay.getDay() + 1) / 7);
  }

  async prevWeek() {
    const d = new Date(this.weekStart);
    d.setDate(d.getDate() - 7);
    this.setWeek(d);
    this.applyFilter();
  }

  async nextWeek() {
    const d = new Date(this.weekStart);
    d.setDate(d.getDate() + 7);
    this.setWeek(d);
    this.applyFilter();
  }

  getSchedules(job: string, day: Date) {
    return this.filtered.filter(s =>
      (s.jobName ?? 'Unknown Job') === job &&
      this.isSameDate(new Date(s.shiftDate), day)
    );
  }

  isSameDate(d1: Date, d2: Date): boolean {
    const a = new Date(d1); a.setHours(0,0,0,0);
    const b = new Date(d2); b.setHours(0,0,0,0);
    return a.getTime() === b.getTime();
  }

  async approve(id: number) {
    await firstValueFrom(this.adminService.approve(id));
    await this.loadSchedules();   // refresh data
  }

  async reject(id: number) {
    await firstValueFrom(this.adminService.reject(id));
    await this.loadSchedules();
  }
}
