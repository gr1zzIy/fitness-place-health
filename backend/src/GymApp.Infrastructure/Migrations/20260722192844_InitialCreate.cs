using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GymApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gyms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    time_zone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gyms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "specializations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specializations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainer_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bio = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                    experience_years = table.Column<int>(type: "integer", nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trainer_profiles", x => x.id);
                    table.CheckConstraint("ck_trainer_profiles_experience_years", "experience_years >= 0");
                    table.ForeignKey(
                        name: "fk_trainer_profiles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gym_branches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gym_branches", x => x.id);
                    table.ForeignKey(
                        name: "fk_gym_branches_gyms_gym_id",
                        column: x => x.gym_id,
                        principalTable: "gyms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "membership_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    duration_days = table.Column<int>(type: "integer", nullable: false),
                    visit_limit = table.Column<int>(type: "integer", nullable: true),
                    is_unlimited = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership_plans", x => x.id);
                    table.CheckConstraint("ck_membership_plans_duration_positive", "duration_days > 0");
                    table.CheckConstraint("ck_membership_plans_price_non_negative", "price >= 0");
                    table.CheckConstraint("ck_membership_plans_visit_limit_positive", "visit_limit IS NULL OR visit_limit > 0");
                    table.ForeignKey(
                        name: "fk_membership_plans_gyms_gym_id",
                        column: x => x.gym_id,
                        principalTable: "gyms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "training_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    default_duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    default_capacity = table.Column<int>(type: "integer", nullable: true),
                    format = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_types", x => x.id);
                    table.CheckConstraint("ck_training_types_capacity_positive", "default_capacity IS NULL OR default_capacity > 0");
                    table.CheckConstraint("ck_training_types_duration_positive", "default_duration_minutes > 0");
                    table.ForeignKey(
                        name: "fk_training_types_gyms_gym_id",
                        column: x => x.gym_id,
                        principalTable: "gyms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trainer_specializations",
                columns: table => new
                {
                    trainer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialization_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trainer_specializations", x => new { x.trainer_profile_id, x.specialization_id });
                    table.ForeignKey(
                        name: "fk_trainer_specializations_specializations_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "specializations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_trainer_specializations_trainer_profiles_trainer_profile_id",
                        column: x => x.trainer_profile_id,
                        principalTable: "trainer_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gym_rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gym_rooms", x => x.id);
                    table.CheckConstraint("ck_gym_rooms_capacity_positive", "capacity > 0");
                    table.ForeignKey(
                        name: "fk_gym_rooms_gym_branches_gym_branch_id",
                        column: x => x.gym_branch_id,
                        principalTable: "gym_branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "client_memberships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    membership_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starts_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    visit_limit = table.Column<int>(type: "integer", nullable: true),
                    visits_used = table.Column<int>(type: "integer", nullable: false),
                    purchased_price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_memberships", x => x.id);
                    table.CheckConstraint("ck_client_memberships_price_non_negative", "purchased_price >= 0");
                    table.CheckConstraint("ck_client_memberships_valid_period", "expires_at > starts_at");
                    table.CheckConstraint("ck_client_memberships_visit_limit_positive", "visit_limit IS NULL OR visit_limit > 0");
                    table.CheckConstraint("ck_client_memberships_visits_used_non_negative", "visits_used >= 0");
                    table.CheckConstraint("ck_client_memberships_visits_within_limit", "visit_limit IS NULL OR visits_used <= visit_limit");
                    table.ForeignKey(
                        name: "fk_client_memberships_asp_net_users_client_user_id",
                        column: x => x.client_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_client_memberships_membership_plans_membership_plan_id",
                        column: x => x.membership_plan_id,
                        principalTable: "membership_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "training_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_branch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gym_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trainer_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starts_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ends_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    cancellation_reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_sessions", x => x.id);
                    table.CheckConstraint("ck_training_sessions_capacity_positive", "capacity > 0");
                    table.CheckConstraint("ck_training_sessions_valid_period", "ends_at > starts_at");
                    table.ForeignKey(
                        name: "fk_training_sessions_gym_rooms_gym_room_id",
                        column: x => x.gym_room_id,
                        principalTable: "gym_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_training_sessions_trainer_profiles_trainer_profile_id",
                        column: x => x.trainer_profile_id,
                        principalTable: "trainer_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_training_sessions_training_types_training_type_id",
                        column: x => x.training_type_id,
                        principalTable: "training_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    booked_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    cancelled_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    cancellation_reason = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookings_asp_net_users_client_user_id",
                        column: x => x.client_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_bookings_training_sessions_training_session_id",
                        column: x => x.training_session_id,
                        principalTable: "training_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    training_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    booking_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    checked_in_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    checked_out_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    marked_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendances", x => x.id);
                    table.ForeignKey(
                        name: "fk_attendances_asp_net_users_client_user_id",
                        column: x => x.client_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attendances_asp_net_users_marked_by_user_id",
                        column: x => x.marked_by_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attendances_bookings_booking_id",
                        column: x => x.booking_id,
                        principalTable: "bookings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_attendances_training_sessions_training_session_id",
                        column: x => x.training_session_id,
                        principalTable: "training_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_attendances_booking_id",
                table: "attendances",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_attendances_client_user_id",
                table: "attendances",
                column: "client_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendances_marked_by_user_id",
                table: "attendances",
                column: "marked_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendances_training_session_id",
                table: "attendances",
                column: "training_session_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendances_training_session_id_client_user_id",
                table: "attendances",
                columns: new[] { "training_session_id", "client_user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bookings_client_user_id",
                table: "bookings",
                column: "client_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_status",
                table: "bookings",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_training_session_id",
                table: "bookings",
                column: "training_session_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_training_session_id_client_user_id",
                table: "bookings",
                columns: new[] { "training_session_id", "client_user_id" },
                unique: true,
                filter: "status IN (1, 3)");

            migrationBuilder.CreateIndex(
                name: "ix_client_memberships_client_user_id",
                table: "client_memberships",
                column: "client_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_memberships_client_user_id_status",
                table: "client_memberships",
                columns: new[] { "client_user_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_client_memberships_expires_at",
                table: "client_memberships",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "ix_client_memberships_membership_plan_id",
                table: "client_memberships",
                column: "membership_plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_gym_branches_gym_id_name",
                table: "gym_branches",
                columns: new[] { "gym_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gym_rooms_gym_branch_id_name",
                table: "gym_rooms",
                columns: new[] { "gym_branch_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_gyms_name",
                table: "gyms",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_membership_plans_gym_id_name",
                table: "membership_plans",
                columns: new[] { "gym_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_specializations_name",
                table: "specializations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trainer_profiles_user_id",
                table: "trainer_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trainer_specializations_specialization_id",
                table: "trainer_specializations",
                column: "specialization_id");

            migrationBuilder.CreateIndex(
                name: "ix_training_sessions_gym_room_id_starts_at",
                table: "training_sessions",
                columns: new[] { "gym_room_id", "starts_at" });

            migrationBuilder.CreateIndex(
                name: "ix_training_sessions_status",
                table: "training_sessions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_training_sessions_trainer_profile_id_starts_at",
                table: "training_sessions",
                columns: new[] { "trainer_profile_id", "starts_at" });

            migrationBuilder.CreateIndex(
                name: "ix_training_sessions_training_type_id_starts_at",
                table: "training_sessions",
                columns: new[] { "training_type_id", "starts_at" });

            migrationBuilder.CreateIndex(
                name: "ix_training_types_gym_id_name",
                table: "training_types",
                columns: new[] { "gym_id", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropTable(
                name: "client_memberships");

            migrationBuilder.DropTable(
                name: "trainer_specializations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "membership_plans");

            migrationBuilder.DropTable(
                name: "specializations");

            migrationBuilder.DropTable(
                name: "training_sessions");

            migrationBuilder.DropTable(
                name: "gym_rooms");

            migrationBuilder.DropTable(
                name: "trainer_profiles");

            migrationBuilder.DropTable(
                name: "training_types");

            migrationBuilder.DropTable(
                name: "gym_branches");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "gyms");
        }
    }
}
