import { DetailDemo } from "./DetailDemo";

export interface project {
  simpleName: string;
  role: string;
  name: string;
  duration: string;
  type: string;
  detail: string;
  achievement: string;

  techStack: string;
  demo: DetailDemo[];
}
