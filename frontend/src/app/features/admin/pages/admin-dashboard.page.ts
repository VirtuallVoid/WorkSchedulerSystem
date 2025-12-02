import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AdminService } from '../../../core/services/admin.service';
import { Schedule } from '../../../shared/models/schedule.model';
import { DatePipe, NgClass, CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.page.html',
  imports: [
    DatePipe,
    NgClass,
    CommonModule
  ],
  styleUrls: ['./admin-dashboard.page.scss']
})
export class AdminPage implements OnInit {

  schedules: Schedule[] = [];          // Raw data
  filtered: Schedule[] = [];           // Only current week
  jobs: string[] = [];
  days: Date[] = [];

  weekStart!: Date;
  weekEnd!: Date;
  weekNumber!: number;

  constructor(private adminService: AdminService,
              private cdr: ChangeDetectorRef) {}


  async ngOnInit() {
    this.initializeCurrentWeek();
    await this.loadSchedules();   // no need for setTimeout
  }

  async loadSchedules() {
    const res = await firstValueFrom(this.adminService.getAllSchedules());

    this.schedules = res || [];

    this.applyFilter();   // 🔥 filter by current week

    this.jobs = [...new Set(
      this.filtered.map(x => x.jobName ?? 'Unknown Job')
    )];
    this.cdr.detectChanges();
  }

  applyFilter() {
    this.filtered = this.schedules.filter(s => {
      const start = new Date(s.startDate);
      const end = new Date(s.endDate);

      return (
        end >= this.weekStart &&
        start <= this.weekEnd
      );
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
    this.applyFilter();   // no need to reload from server
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
      this.isSameDate(new Date(s.startDate), day)
    );
  }

  isSameDate(d1: Date, d2: Date): boolean {
    return (
      d1.getFullYear() === d2.getFullYear() &&
      d1.getMonth() === d2.getMonth() &&
      d1.getDate() === d2.getDate()
    );
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
