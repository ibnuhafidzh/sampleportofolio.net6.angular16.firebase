import { DetailSosmed } from "./DetailSosmed";

export interface About {
  name: string;
  moto: string;
  detail: string;
  roles: string;
  skillfront: string;
  skillback: string;
  skillmobile: string;
  skillsql: string;
  skillnosql: string;
  sosmed: DetailSosmed[];
}
