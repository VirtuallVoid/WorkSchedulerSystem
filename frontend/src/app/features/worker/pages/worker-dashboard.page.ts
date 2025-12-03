import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { WorkerService } from '../../../core/services/worker.service';
import { Schedule } from '../../../shared/models/schedule.model';
import { firstValueFrom } from 'rxjs';
import {CommonModule, DatePipe} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {LogoutButtonComponent} from '../../../shared/logout-button/logout-button.component';

@Component({
  selector: 'app-worker-dashboard',
  templateUrl: './worker-dashboard.page.html',
  imports: [
    DatePipe,
    FormsModule,
    CommonModule,
    LogoutButtonComponent
  ],
  styleUrls: ['./worker-dashboard.page.scss']
})
export class WorkerPage implements OnInit {
  shifts: any[] = [];
  selectedJobId: number | null = null;
  selectedShiftId: number | null = null;
  shiftDate: any;
  message: string = '';
  pendingRequests: Schedule[] = [];
  approvedSchedules: Schedule[] = [];
  showRequests = true;
  showModal = false;
  weekStart!: Date;
  weekEnd!: Date;
  weekNumber!: number;
  days: Date[] = [];

  constructor(private workerService: WorkerService, private cd: ChangeDetectorRef) {}

  async ngOnInit() {
    this.initWeek();
    await this.loadShifts();
    await this.loadMySchedules();
  }

  toggleRequests() {
    this.showRequests = !this.showRequests;
  }

  openModal() {
    this.showModal = true;
    this.cd.detectChanges();
  }

  closeModal() {
    this.showModal = false;
    this.cd.detectChanges();
  }

  async loadShifts() {
    const res = await firstValueFrom(this.workerService.getShiftTypes());
    this.shifts = res.data;
    this.cd.detectChanges();
  }

  async loadMySchedules() {
    const res = await firstValueFrom(this.workerService.getMySchedules());
    const all = res.data;

    this.pendingRequests = all.filter(x => x.statusName === "Pending");
    this.approvedSchedules = all.filter(x => x.statusName === "Approved");
    this.cd.detectChanges();
  }

  async submitRequest() {
    if (!this.selectedShiftId || !this.shiftDate) {
      this.message = "Fill all fields!";
      return;
    }

    const dto = {
      jobId: Number(this.selectedJobId),
      shiftTypeId: Number(this.selectedShiftId),
      shiftDate: this.shiftDate
    };

    const res = await firstValueFrom(this.workerService.submitSchedule(dto));

    this.message = res.message || "Request submitted!";
    await this.loadMySchedules();
    this.closeModal();
  }

  initWeek() {
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

  prevWeek() {
    const d = new Date(this.weekStart);
    d.setDate(d.getDate() - 7);
    this.setWeek(d);
  }

  nextWeek() {
    const d = new Date(this.weekStart);
    d.setDate(d.getDate() + 7);
    this.setWeek(d);
  }

  getWeekNumber(date: Date) {
    const firstDay = new Date(date.getFullYear(), 0, 1);
    const diff = (+date - +firstDay) / 86400000;
    return Math.ceil((diff + firstDay.getDay() + 1) / 7);
  }

  getApprovedSchedulesByDay(day: Date) {
    const dayOnly = day.toISOString().substring(0, 10);

    return this.approvedSchedules.filter(s => {
      const start = s.shiftDate.substring(0, 10);
      return start === dayOnly;
    });
  }

}
