export interface Schedule {
  id: number;
  userId: number;
  userFullName: string;
  jobId: number;
  jobName: string;
  shiftTypeId: number;
  shiftTypeName: string;
  statusId: number;
  statusName: string;
  shiftDate: string;
  requestDate: string;
  approvedBy: number;
  approvedByFullName: string;
  approvedDate: string;
}
